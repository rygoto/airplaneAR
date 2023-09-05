import functions_framework
import json
from flask import Flask,jsonify

app = Flask(__name__)

@functions_framework.http
def hello_http(request):
    # 各オブジェクトに対する JSON データをリストに格納
    if request.method = 'POST':
    data = request.get_json()

    arg1 = float(data.get('arg1'))

    object_data_list = [
        {
            "position": {"x": arg1, "y": 2.0, "z": 3.0},
            "name": "Object1",
            "move_direction": {"x": 0.1, "y": 0.0, "z": -0.2},
            "other_info": "This is additional information for Object1."
        },
        {
            "position": {"x": 4.0, "y": 5.0, "z": 6.0},
            "name": "Object2",
            "move_direction": {"x": -0.1, "y": 0.0, "z": 0.2},
            "other_info": "This is additional information for Object2."
        },
        {
            "position": {"x": 8.0, "y": 14.0, "z": 9.0},
            "name": "Object3",
            "move_direction": {"x": -0.2, "y": 4.0, "z": 2.2},
            "other_info": "This is additional information for Object3."
        },
        {
            "position": {"x": 8.0, "y": 9.0, "z": 6.0},
            "name": "Object4",
            "move_direction": {"x": -0.9, "y": 6.0, "z": 5.2},
            "other_info": "This is additional information for Object4."
        },
        # 他のオブジェクトのデータも同様に追加
    ]

    # Unity 側に送信する JSON データを選択
    #selected_data = object_data[0]  # この例では最初のオブジェクトのデータを選択

    return jsonify(object_data_list)





    # if request_json:
    #     # JSONデータから必要な情報を取得
    #     position = request_json.get("position", {"x": 0, "y": 0, "z": 0})
    #     name = request_json.get("name", "Unknown")
    #     move_direction = request_json.get("move_direction", {"x": 0, "y": 0, "z": 0})
    #     other_info = request_json.get("other_info", "")

    #     # 応答データを作成
    #     response_data = {
    #         "position": position,
    #         "name": name,
    #         "move_direction": move_direction,
    #         "other_info": other_info
    #     }

    #     # JSON形式でレスポンスを返す
    #     return jsonify(response_data)
    # else:
    #     # リクエストがJSONデータを含まない場合のエラーレスポンス
    #     return "Bad Request: JSON data not provided", 400


    # """HTTP Cloud Function.
    # Args:
    #     request (flask.Request): The request object.
    #     <https://flask.palletsprojects.com/en/1.1.x/api/#incoming-request-data>
    # Returns:
    #     The response text, or any set of values that can be turned into a
    #     Response object using `make_response`
    #     <https://flask.palletsprojects.com/en/1.1.x/api/#flask.make_response>.
    # """
    # request_json = request.get_json(silent=True)
    # request_args = request.args

    # if request_json and 'name' in request_json:
    #     name = request_json['name']
    # elif request_args and 'name' in request_args:
    #     name = request_args['name']
    # else:
    #     name = 'World'
    # return 'Hello {}!'.format(name)
