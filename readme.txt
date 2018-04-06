#Hello Light Field 🕶

####Current build is tested on OSX 10.13.3.

##About Unity app:
Applications main task is to display depthmaps in the correct 3d space. Application detects position of the camera in 3d space, and renders only frame closest to the observer.

Scene LightFields is the main scene in the project. It contains sample based on a file attached on this repo in the Resources file. File 'Resources' needs to be placed on desktop.

MainCamera has components LoadPos and LoadTextures - those scripts has var Limit - it can be changed to adjust to amount of photos in the project folder. Turn on and off Render Closest component, to see all depthma, or just one.
_Codes and _Samples folder, our project folders, rest is external files.

In Resources folder, you will find sample project folder.
This folder needs to be placed on Desktop.


⛅️⛅️⛅️
##Cloud Server settings: 
TO-DO-MARIUSZ

Application should connect to a  adam-cloud-linux-pipeline VM Instance on a Google Cloud Platform. 

SSH Pipeline
--------
0. Upload video you want to convert to lightfield to Desktop/ADAM/USERS/Adam_27.03.18_20.28.30/original_video/video.mp4 Adam_27.03.18_20.28.30

1. Turn on gui VNC:
'vncserver -geometry 1280x1024
nc 35.190.142.59 5901'

2. Cut video to frames, by using this command:
'ffmpeg -i $1 -r 25/1 -s 256x192 /home/funkylogic_studio/Desktop/ADAM/USERS/$2/frame_256x192/frame-%06d.color.png'
Where $1 is the name of the  video. 

3.Calculate depth by going to location; /home/funkylogic_studio/Desktop/ADAM/demon/examples directory, and running script:
'python3 loop.py /home/funkylogic_studio/Desktop/ADAM/USERS/Adam_27.03.18_20.28.30/frame_256x192 '
By default scripts works with 800 frames. If you want to edit number of frames to id in loop.py script.

4. Send proj files to run them in Unity.

Trello board with more info about pipeline and cloud:
https://trello.com/b/O7S5iMgg/lightfield-capture

Current Status
--------------
Current version of this software allows users to manually go through each step. Now the task is to autmate it - so users can send the video by www or webiste to the cloud, and after a while see it in form of LightField - sequence of depthmaps with dynamic textures.


