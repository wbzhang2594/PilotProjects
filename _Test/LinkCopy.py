import os

SourcePath="D:\\_Test\\CPLink\\F1\\"
TargetPath="D:\\_Test\\CPLink\\F2\\"

for rt, dirs, files in os.walk(SourcePath):
    for f in files:
        print("rt", rt)
        print("dirs", dirs)
        print("files", files)
        print("=========")
        f_FullName = os.path.join(rt,f)
        f_RalativeName = os.path.relpath(f_FullName,SourcePath)
    
    

