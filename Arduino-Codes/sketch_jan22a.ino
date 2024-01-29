
// Data Transfer
String oldData;

//led pins
int ledStatus[10];
int ledPins[10] = {2,3,4,5,6,7,8,9,10,11};
int ldrPin = A0;

void setup() {
  Serial.begin(9600);
  for(int i =0;i<10;i++)
  {
    pinMode(ledPins[i], OUTPUT);
  }
  pinMode(ldrPin,INPUT);
}

void loop() {
  Led();
}

void sendData(String data) {
  if (data != oldData) {
    Serial.println(data);
    oldData = data;
  }
}

int readData() {
  String rawData = Serial.readString();
  int data[10];
  if (rawData.length() > 2) {
  splitString(rawData, ',', data, 10);
  for(int i=0;i<11;i++)
  { 
      ledStatus[i] = data[i];
  }  
  }
}

int splitString(String input, char delimiter, int integers[], int maxIntegers) {
  int index = 0;      // Index for the integers array
  int fromIndex = 0;  // Starting index for searching the delimiter

  while (true) {
    int delimiterIndex = input.indexOf(delimiter, fromIndex);

    if (delimiterIndex == -1) {
      // No more delimiters found, convert the remaining string to integer
      integers[index++] = input.substring(fromIndex).toInt();
      break;
    }

    // Convert the substring between fromIndex and delimiterIndex to integer
    integers[index++] = input.substring(fromIndex, delimiterIndex).toInt();

    // Update fromIndex to search for the next delimiter
    fromIndex = delimiterIndex + 1;

    // Check if the maximum number of integers has been reached
    if (index >= maxIntegers) {
      break;
    }
  }

  return index;  // Return the number of integers
}


void Led()
{
  readData();

  for(int i = 0; i<10;i++)
  {
    digitalWrite(ledPins[i],ledStatus[i]);
  }
}

void LDR()
{
  float ldrData = analogRead(ldrPin);

  if(ldrData <=600)
  {
    sendData("1");
  }
  else
  {
    sendData("0");
  }
}
