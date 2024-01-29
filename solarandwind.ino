#include <Servo.h>
#include <AccelStepper.h>

// Solar pins
Servo horizontal;
Servo vertical;
int servov = 45;
int servoh = 45;
int servohLimitHigh = 100;
int servohLimitLow = 1;
int servovLimitHigh = 100;
int servovLimitLow = 1;

// WindMill Pins
AccelStepper stepperWindmill(AccelStepper::FULL4WIRE, 8, 9, 10, 11);
unsigned long previousMillis = 0;
const long interval = 100000;

int ldrlt = A3;
int ldrrt = A2;
int ldrld = A0;
int ldrrd = A1;

void setup() {
  horizontal.attach(3);
  horizontal.write(servoh);

  stepperWindmill.setMaxSpeed(1000.0);
  stepperWindmill.setAcceleration(500.0);

  vertical.attach(2);
  vertical.write(servov);


  stepperWindmill.moveTo(0);
  stepperWindmill.runToPosition();

  delay(2500);
}

void Solar() {
  int lt = analogRead(ldrlt);
  int rt = analogRead(ldrrt);
  int ld = analogRead(ldrld);
  int rd = analogRead(ldrrd);
  int dtime = 10;
  int tol = 90;
  int avt = (lt + rt) / 2;
  int avd = (ld + rd) / 2;
  int avl = (lt + ld) / 2;
  int avr = (rt + rd) / 2;
  int dvert = avt - avd;
  int dhoriz = avl - avr;

  if (-1 * tol > dvert || dvert > tol) {
    if (avt > avd) {
      servov = ++servov;
      if (servov > servovLimitHigh) { servov = servovLimitHigh; }
    } else if (avt < avd) {
      servov = --servov;
      if (servov < servovLimitLow) { servov = servovLimitLow; }
    }
    vertical.write(servov);
  }

  if (-1 * tol > dhoriz || dhoriz > tol) {
    if (avl > avr) {
      servoh = ++servoh;
      if (servoh > servohLimitHigh) { servoh = servohLimitHigh; }
    } else if (avl < avr) {
      servoh = --servoh;
      if (servoh < servohLimitLow) { servoh = servohLimitLow; }
    }

    horizontal.write(servoh);
  }
 delay(dtime);
 
}

void Windmill() {
  unsigned long currentMillis = millis();
  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;

    int randomDirection = random(0, 2);
    int steps = 200;

    if (randomDirection == 0) {
      stepperWindmill.moveTo(stepperWindmill.currentPosition() - steps);
    } else {
      stepperWindmill.moveTo(stepperWindmill.currentPosition() + steps);
    }
  } 

  stepperWindmill.run();
}

void loop() {
  Solar();
  Windmill();
}