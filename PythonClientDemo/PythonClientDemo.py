#coding:utf-8
import requests

requests.post("http://192.168.1.8:9000/receiveimg",open("d:\\4.jpg","rb"))
print('上传图片完成')

requests.post("http://192.168.1.8:9000/receiveimage/22",open("d:\\4.jpg","rb"))

print('上传图片完成（带指定文件名）')

import shutil

res = requests.get("http://192.168.1.8:9000/readimg",stream=True)
with open('d:\\download.jpg','wb') as outfile:
    shutil.copyfileobj(res.raw, outfile)

del res

print('图片下载完成')

filename = "penguins.jpg";
res = requests.get("http://192.168.1.8:9000/readimage/"+ filename,stream=True)
with open('d:\\'+ filename,'wb') as outfile:
    shutil.copyfileobj(res.raw, outfile)

del res

print('图片下载完成(带指定文件名)')

