import os
import hashlib

root = "D:\\TFS\\201510\\_ServerBuild_sim_0_bak_0\\"	#要遍历的目录
root2 = "D:\\TFS\\201510\\_ServerBuild_sim_1_EMChanged_1\\"	#要遍历的目录
root_source = "D:\\_Test\\TestFileCopy\\F1\\"
root_target = "D:\\_Test\\TestFileCopy\\F2\\"


def hashfile(filepath):
    sha1 = hashlib.sha1()
    f = open(filepath, 'rb')
    try:
        sha1.update(f.read())
    finally:
        f.close()
    return sha1.hexdigest()


def CalculateHashOfAllFiles(path):
    Dic = {}
    for rt, dirs, files in os.walk(path):
        for f in files:
            f_FullName = os.path.join(rt,f)
            f_RalativeName = os.path.relpath(f_FullName,path)
            #print('f_RalativeName', f_RalativeName)
            Dic[f_RalativeName] = [f, f_RalativeName, hashfile(f_FullName)]
            #print(Dic[f_FullName])
    return Dic


def GroupFile(dic, fileName, fileRelativeName, description):
    if(fileRelativeName in dic):
        dic[fileName]+=([fileRelativeName,description],)
    else:
        dic[fileName] = ([fileRelativeName,description],)

def AddFileDiscription(Dic, fileRelativeName, Description):
    Dic[fileRelativeName] = Description

def IsSameFile(file1_SHA1, file2_SHA1):
    if(file1_SHA1 == file2_SHA1):
        return True
    else:
        return False


def CheckDelta(Dic_Source, Dic_Target, Description):
    Dic_Delta = {}
    for fileRelativeName in Dic_Source.keys():
        if(fileRelativeName in Dic_Target):
            if(False == IsSameFile(Dic_Source[fileRelativeName][2],Dic_Target[fileRelativeName][2])):
                AddFileDiscription(Dic_Delta, fileRelativeName, Description)
        else:
            AddFileDiscription(Dic_Delta, fileRelativeName, Description)

    return Dic_Delta



Dic1 = CalculateHashOfAllFiles(root_source)
Dic2 = CalculateHashOfAllFiles(root_target)
#print(Dic1)
#print(Dic2)

## S => T
Dic_To_Add = CheckDelta(Dic1,Dic2,"Add")

## T => S
Dic_To_Del = CheckDelta(Dic2,Dic1,"Del")

## S
#Dic_Source_TheSame = GetSame(Dic1)

##        if(Dic2[fileRelativeName][2]==Dic1[fileRelativeName][2]):
##            fileName=Dic1[fileRelativeName][0]
##            if(fileName in Dic3):
##                Dic3[fileName]+=([fileRelativeName,'Same'],)
##            else:
##                Dic3[fileName]=([fileRelativeName,'Same'],)
print(Dic_To_Add)


