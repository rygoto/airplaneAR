from flask import Flask, request, jsonify
import numpy as np
import json
from FlightRadar24 import FlightRadar24API
import math
import random  # randomモジュールをインポート

app = Flask(__name__)

fr_api = FlightRadar24API(...)  # FlightRadar24APIの設定を追加


def extract_first_two_words(name):
    words = name.split()
    if len(words) >= 2:
        return " ".join(words[:2])
    else:
        return name


# flightradarapiからのデータ取得を試行し、エラーが発生した場合に代替データを格納する関数
def get_flight_data(latitude, longitude, radius):
    try:
        selected_flights = []
        for flight in fr_api.get_flights():
            flight_latitude = flight.latitude
            flight_longitude = flight.longitude
            distance = np.sqrt(
                (flight_latitude - latitude) ** 2 + (flight_longitude - longitude) ** 2
            )
            if distance <= (radius / 111.32):
                selected_flights.append(flight)

        flight_data = []
        for new_flight in selected_flights:
            details = fr_api.get_flight_details(new_flight.id)
            new_flight.set_flight_details(details)

            # 情報の変換
            x = (new_flight.longitude - longitude) * (radius / 111.32)
            z = (new_flight.latitude - latitude) * (radius / 111.32)

            heading_degrees = new_flight.heading
            heading_radians = math.radians(heading_degrees)
            heading_x = math.cos(heading_radians)
            heading_z = math.sin(heading_radians)

            origin_name = new_flight.origin_airport_name
            origin_code = extract_first_two_words(origin_name)

            destination_name = new_flight.destination_airport_name
            destination_code = extract_first_two_words(destination_name)

            # 格納
            new_flight.unity_x = x
            new_flight.unity_z = z
            new_flight.unity_heading_x = heading_x
            new_flight.unity_heading_z = heading_z
            new_flight.new_origin = origin_code
            new_flight.new_destination = destination_code

            data = {
                "airline_short_name": new_flight.airline_short_name,
                "new_origin": new_flight.new_origin,
                "new_destination": new_flight.new_destination,
                "unity_coordinates": f"({new_flight.unity_x},{new_flight.unity_z})",
                "unity_heading": f"({new_flight.unity_heading_x},{new_flight.unity_heading_z})",
                "longitude_latitude": f"({new_flight.longitude},{new_flight.latitude})",
            }

            flight_data.append(data)

        return flight_data

    except Exception as e:
        print(f"Error fetching flight data from FlightRadar24 API: {str(e)}")
        # エラーが発生した場合に代替データを生成
        alternative_data = []
        # これが定義じゃないのか　スコープが違う
        for _ in range(5):
            new_flight = {}

            new_flight["airline_short_name"] = random.choice(
                ["ANA", "JAL", "CHUO-SPECIAL"]
            )
            new_flight["new_destination"] = random.choice(
                ["OOSAKI", "SOKA", "ZOSHIGAYA"]
            )
            new_flight["new_origin"] = random.choice(
                ["OOKUBO", "HAKATA", "MEMANBETSU", "SHIMOSHIMMEI"]
            )

            unko = 1 / 111.32
            a_random = latitude + random.uniform(-unko, unko)
            b_random = longitude + random.uniform(-unko, unko)
            new_flight["longitude_latitude"] = f"({b_random},{a_random})"

            heading_degrees = random.uniform(0, 360)
            heading_radians = math.radians(heading_degrees)
            new_flight[
                "unity_heading"
            ] = f"({math.cos(heading_radians)},{math.sin(heading_radians)})"

            scale = 100
            x = (b_random - longitude) * (radius / 111.32) * scale
            z = (a_random - latitude) * (radius / 111.32) * scale
            new_flight["unity_x"] = x
            new_flight["unity_z"] = z

            alternative_data.append(new_flight)

        return alternative_data


@app.route("/", methods=["GET"])
def hello_http(request):
    latitude = float(request.args.get("lat"))
    longitude = float(request.args.get("lon"))
    radius = float(request.args.get("r"))

    # try:
    #     flight_data = get_flight_data(latitude, longitude, radius)
    #     return jsonify(flight_data)
    # except Exception as e:
    #     print(f"Error fetching flight data: {str(e)}")
    #     alternative_data = get_flight_data(latitude, longitude, radius)
    #     return jsonify(alternative_data)

    flight_data = get_flight_data(latitude, longitude, radius)
    return jsonify(flight_data)
    # ctrl + /でコメントアウトできる　定義されてないからだ
    # というよりget_flight_dataでエラーハンドリングしてるから
    # ここではtry except しなくていいほうほう
    # もしget_flight_data内でエラーが起きてもreturn alternative_dataされるから
    # get_flight_dataの呼び出し側ではエラーは絶対に起きない
    # そもそもこっちじゃalternative_dataは定義されてない！
    # これだけでいい
    # だってget_flight_data内でエラーが起きてもreturn alternative_dataされるだろ！
    # pythonよくわからんがされると思う・・・

    # ここなんでダメなんや
    # 　たしかし。そしたらこっちでflight_dataとalternative_dataを分岐
    # これでええんか？？　alternative_dataもflight_dataに格納されるんか？あぎゃす
    # これでやってみるぜ


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
