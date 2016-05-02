TITAN 1 FUNCTIONALITY

TITAN 1 is the next generation of space wearables, since it will allows you to interact with your data from a whole new point of view.

Titan consist of a helmet running with the new UDOO x86 development board from intel running an app made with Unity 3D that will use Augmented reality technology to display all the menus and data. The display will be a transparent OLED flexible screen attached to the helmet from the inside.

Titan will be made of metal strip that will retract using two pancake motors attached to each side of the helmet, it will also include an HD camera in top of the visor and 2 Ultra bright LEDs 

Our wearable will also have a bracelet that will be the astronaut command center. In there he will be able to access the Augmented reality features with the markers we have implemented. This markers will be associated with certain functionalities that will request data from 1 or many arduino boards.


In the home screen the astronaut will be able to check his biometrics:  Amount of Oxygen, suit temperature, outside temperature, magnetic fields, battery life, heart beat amongst other. He will also be able to see a picture ID

![ScreenShot](https://raw.github.com/DharmaProject/titan1/biometrics screen.png)

Aside from that we have included 5 multi purpose buttons that will help the astronaut navigate through several tools and gadgets. The obes we have identified are:

SPACE GPS (PLANET CATALOG)
In here the astronaut will be able to access information about where he is at or the sottlar system.  Also a  guide of planets will be presented where the astronaut will be able to select a particular planet and obtain general information about its composition, atmosphere, size, distance, age, etc.

MISSION CONTROL

In this part the astronaut will be able to browse a database with relevant information for the mission such as manuals, guidelines, 3D models of machines, motors , etc 

 

ROBOT ARM

In this menu a control will show up showing an arm that will be on the spaceship or a back pack

COMPOUND ANALYZER

The suit will have a small device capable of analyzing some components t9 detect hazardous materials for the astronaut and display in a chart the concentration of each element included in the sample

MUSIC PLAYER 

For those stressful mission, there is nothing better than a smooth jazz or for those intense activities some Iron Maiden. That is why we have included a music player.

System specs

Unity 3D
Arduino IDE
Vuforia library for Augmented reality

Hardware 

OLED screen
UDOO x86 for running the app
Arduino 101 or UNO to gather the information from the sensors
HD cam to detect the markers
TMP36 to sense the ambient temperature
O2 sensor (In our protoype we used a flow meter)
2 ultrabgright 5mm white LEDs
1 10mm difussed RGB common Anode to have a visual indicator for the oxygen
MPR121 12 key capacity sensor to use  multifunctional buttons

