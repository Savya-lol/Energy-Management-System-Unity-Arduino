<h1><b>Energy Management Sysem (EMS)</b></h1>
<h2><b>Introduction</b></h2>
Energy Crisis is one of the problems which is plaguing this world in the current age. Our Energy Management System (EMS) project, if implemented and developed further can solve the problem with high efficiency.
To show the prototype of our Energy management system, we will be using the integration of sensors and Software. Our hardware system, that will show the prototype of a city implementing our system, will consist of solar panels, servo motors, sensors, leds and other hardware components controlled by an Arduino. Our Software system on the other hand, will be made using Unity Engine which is a software used to build applications and video 
<h2><b>Software Used</b></h2>
<ul>
        <li>Unity Engine 2022.3.12f1</li>
        <li>Arduino IDE</li>
        <li>Visual Studio</li>
</ul>
<h2><b>Hardware Used</b></h2>
<ul>
        <li>Arduino UNO</li>
        <li>LEDs</li>
        <li>Solar Panel</li>
        <li>2 Servo Motors</li>
        <li>motor</li>
        <li>wires</li>
        <li>LIPO 2200mAh</li>
</ul>
<h2><b>Hardware Instructions</b></h2>
We created a sun tracking solar panel with the following design:<BR>
[Solar_Image]<BR><BR>
The panel turns to where ever the sunlight is at the maximum.
<BR><BR>
We then added a windmill model on our project with the following design:<BR>
[Windmill_Image]<BR><BR>
The windmill's propeller when spinned, triggers a led to glow to indicate the power generated.
<BR><BR>
We then put some LEDs into street lamps controlled by an LDR. We put the other street lamps inside our house models and created a model of a city with two parts, where the power is seperatly distributed. We then connnected all the parts into our LIPO battery to power them but powered the Arduino through a USB since we also need to transfer data betweeen Arduino and Unity. The result looks like this:<BR>
[city_combined_image]

<h2><b>Software Instructions</b></h2>
Connect arduino to your computer using a programming cable then, open <I>Scenes->Sample Scene</I>. Then click on a GameObject called <I>SerialManager</I> and enter the name of your Arduino's port and click play.

<h2><b>Expected Output</b></h2>
You should now see a prototype of city using an Energy Management System, which includes a software, a smart solar panel and a windmill. The software is used to monitor and control all the electricity of the city model. The more lights are turned on, the more energy is consumed and if there will be spare energy, it will be stored in the battery which will be useful when there will be breakdown in the power supply due to maintainance or natural causes. The spare power can be supplied to other places or can be sold to neighbouring nations as well. This system reduces the waste of energy and also reduces problems like power outages and can also be suitable for place where generating hydroelctricity isn't feasible.
