import cv2
from cvzone.PoseModule import PoseDetector
import socket
import mediapipe as mp
# from bvh import Bvh
# from numpy import np
# import time


# cap = cv2.VideoCapture('1.mp4')

cap=cv2.VideoCapture(0) # Open Camera
cap.open(0) # Keep Camera Open

# time.sleep(0.5)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5055)


detector = PoseDetector()
posList = []


lmString = ''
mp_drawing = mp.solutions.drawing_utils

# def create_bvh_structure():
#     # 这里只是一个示例骨架，你需要根据实际的姿势数据来定义
#     skeleton = """
#     HIERARCHY
#     ROOT Hips
#     {
#         OFFSET -0.001795 -0.223333 0.028219
#         CHANNELS 6 Xposition Yposition Zposition Zrotation Yrotation Xrotation 
#         JOINT LeftUpLeg
#         {
#             OFFSET 0.069520 -0.091406 -0.006815
#             CHANNELS 3 Zrotation Yrotation Xrotation
#             JOINT LeftLeg
#             {
#                 OFFSET 0.034277 -0.375199 -0.004496
#                 CHANNELS 3 Zrotation Yrotation Xrotation
#                 JOINT LeftFoot
#                 {
#                     OFFSET -0.013596 -0.397961 -0.043693
#                     CHANNELS 3 Zrotation Yrotation Xrotation
#                     JOINT LeftToe
#                     {
#                         OFFSET 0.026358 -0.055791 0.119288
#                         CHANNELS 3 Zrotation Yrotation Xrotation
#                         End Site
#                         {
#                             OFFSET 0.000000 0.000000 0.000000
#                         }
#                     }
#                 }
#             }
#         }
#         JOINT RightUpLeg
#         {
#             OFFSET -0.067670 -0.090522 -0.004320
#             CHANNELS 3 Zrotation Yrotation Xrotation
#             JOINT RightLeg
#             {
#                 OFFSET -0.038290 -0.382569 -0.008850
#                 CHANNELS 3 Zrotation Yrotation Xrotation
#                 JOINT RightFoot
#                 {
#                     OFFSET 0.015774 -0.398415 -0.042312
#                     CHANNELS 3 Zrotation Yrotation Xrotation
#                     JOINT RightToe
#                     {
#                         OFFSET -0.025372 -0.048144 0.123348
#                         CHANNELS 3 Zrotation Yrotation Xrotation
#                         End Site
#                         {
#                             OFFSET 0.000000 0.000000 0.000000
#                         }
#                     }
#                 }
#             }
#         }
#         JOINT Spine
#         {
#             OFFSET -0.002533 0.108963 -0.026696
#             CHANNELS 3 Zrotation Yrotation Xrotation
#             JOINT Spine1
#             {
#                 OFFSET 0.005487 0.135180 0.001092
#                 CHANNELS 3 Zrotation Yrotation Xrotation
#                 JOINT Spine2
#                 {
#                     OFFSET 0.001457 0.052922 0.025425
#                     CHANNELS 3 Zrotation Yrotation Xrotation
#                     JOINT Neck
#                     {
#                         OFFSET -0.002778 0.213870 -0.042857
#                         CHANNELS 3 Zrotation Yrotation Xrotation
#                         JOINT Head
#                         {
#                             OFFSET 0.005152 0.064970 0.051349
#                             CHANNELS 3 Zrotation Yrotation Xrotation
#                             End Site
#                             {
#                                 OFFSET 0.000000 0.000000 0.000000
#                             }
#                         }
#                     }
#                     JOINT LeftShoulder
#                     {
#                         OFFSET 0.078845 0.121749 -0.034090
#                         CHANNELS 3 Zrotation Yrotation Xrotation
#                         JOINT LeftArm
#                         {
#                             OFFSET 0.090977 0.030469 -0.008868
#                             CHANNELS 3 Zrotation Yrotation Xrotation
#                             JOINT LeftForeArm
#                             {
#                                 OFFSET 0.259612 -0.012772 -0.027456
#                                 CHANNELS 3 Zrotation Yrotation Xrotation
#                                 JOINT LeftHand
#                                 {
#                                     OFFSET 0.249234 0.008986 -0.001171
#                                     CHANNELS 3 Zrotation Yrotation Xrotation
#                                     End Site
#                                     {
#                                         OFFSET 0.000000 0.000000 0.000000
#                                     }
#                                 }
#                             }
#                         }
#                     }
#                     JOINT RightShoulder
#                     {
#                         OFFSET -0.081759 0.118833 -0.038615
#                         CHANNELS 3 Zrotation Yrotation Xrotation
#                         JOINT RightArm
#                         {
#                             OFFSET -0.096012 0.032551 -0.009143
#                             CHANNELS 3 Zrotation Yrotation Xrotation
#                             JOINT RightForeArm
#                             {
#                                 OFFSET -0.253742 -0.013329 -0.021401
#                                 CHANNELS 3 Zrotation Yrotation Xrotation
#                                 JOINT RightHand
#                                 {
#                                     OFFSET -0.255298 0.007772 -0.005559
#                                     CHANNELS 3 Zrotation Yrotation Xrotation
#                                     End Site
#                                     {
#                                         OFFSET 0.000000 0.000000 0.000000
#                                     }
#                                 }
#                             }
#                         }
#                     }
#                 }
#             }
#         }
#     }
#     MOTION
#     Frames: 196
#     Frame Time: 0.050000

#     """
#     return Bvh(skeleton)

# def add_frame_to_bvh(bvh, frame_data):
#     # 添加帧数据到BVH对象
#     bvh.add_frame(frame_data)


if not (cap.isOpened()):
    print("Could not open video device")
while True:
#     bvh = create_bvh_structure()
    success, img = cap.read()
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img)
    if bboxInfo:
        lmString = ''
        for lm in lmList:
            print(lm)
            lmString += f'{lm[0]}, {img.shape[0] - lm[1]}, {lm[2]},'
        posList.append(lmString)        

    # print(posList)

    data = lmString
    sock.sendto(str.encode(str(data)), serverAddressPort)

    cv2.imshow("Image", img)
    key = cv2.waitKey(1)
    if key == ord('s'):
        with open("AnimationFile.txt", "w") as f:
            f.writelines(["%s\n" % item for item in posList])
            # add_frame_to_bvh(bvh, posList)
            # f.write(bvh.dumps())
    if key == 27: # Press Esc to close camera
        break
    

cap.release()
cv2.destroyAllWindows()

'''
if key == ord('q'):
    # exit()
    break

'''
