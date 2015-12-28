import os

path="D:\\_Test\\CPLink\\"

for rt, dirs, files in os.walk(path):
    print(rt)
    print(dirs)
    print(files)
