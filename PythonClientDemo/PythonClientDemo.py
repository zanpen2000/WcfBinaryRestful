#coding:utf-8
import requests

requests.post("http://192.168.1.8:9000/receiveimg",open("d:\\4.jpg","rb"))
print('�ϴ�ͼƬ���')

requests.post("http://192.168.1.8:9000/receiveimage/22",open("d:\\4.jpg","rb"))

print('�ϴ�ͼƬ��ɣ���ָ���ļ�����')

import shutil

res = requests.get("http://192.168.1.8:9000/readimg",stream=True)
with open('d:\\download.jpg','wb') as outfile:
    shutil.copyfileobj(res.raw, outfile)

del res

print('ͼƬ�������')

filename = "penguins.jpg";
res = requests.get("http://192.168.1.8:9000/readimage/"+ filename,stream=True)
with open('d:\\'+ filename,'wb') as outfile:
    shutil.copyfileobj(res.raw, outfile)

del res

print('ͼƬ�������(��ָ���ļ���)')

