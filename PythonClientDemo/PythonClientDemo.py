import requests

#requests.post("http://192.168.1.8:9000/receiveimg",open("d:\\bdbfa376-a96f-416b-be8d-5898a464afb1.jpg","rb"))
requests.post("http://192.168.1.8:9000/receiveimg",open("d:\\4.jpg","rb"))

print('picture upload done!')

import shutil

res = requests.get("http://192.168.1.8:9000/readimg",stream=True)
with open('d:\\download.jpg','wb') as outfile:
    shutil.copyfileobj(res.raw, outfile)

del res

print('picture download done!')
