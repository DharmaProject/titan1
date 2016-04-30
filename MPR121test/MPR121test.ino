/*********************************************************
This is a library for the MPR121 12-channel Capacitive touch sensor

Designed specifically to work with the MPR121 Breakout in the Adafruit shop 
  ----> https://www.adafruit.com/products/

These sensors use I2C communicate, at least 2 pins are required 
to interface

Adafruit invests time and resources providing this open source code, 
please support Adafruit and open-source hardware by purchasing 
products from Adafruit!

Written by Limor Fried/Ladyada for Adafruit Industries.  
BSD license, all text above must be included in any redistribution
**********************************************************/

#include <Wire.h>
#include "Adafruit_MPR121.h"

// You can have up to 4 on one i2c bus but one is enough for testing!
Adafruit_MPR121 cap = Adafruit_MPR121();

// Keeps track of the last pins touched
// so we know when buttons are 'released'
uint16_t lasttouched = 0;
uint16_t currtouched = 0;
int led = 3;
int pot = A0;
int sensorValue;
int fade;
int estado;

void setup() {

  pinMode(led, OUTPUT);
  pinMode(pot, INPUT);
  
  while (!Serial);        // needed to keep leonardo/micro from starting too fast!

  Serial.begin(9600);
 // Serial.println("Adafruit MPR121 Capacitive Touch sensor test"); 
  
  // Default address is 0x5A, if tied to 3.3V its 0x5B
  // If tied to SDA its 0x5C and if SCL then 0x5D
  if (!cap.begin(0x5A)) {
    //Serial.println("MPR121 not found, check wiring?");
    while (1);
  }
  //Serial.println("MPR121 found!");
}

void loop() {
   sensorValue= analogRead(pot);
  // Get the currently touched pads
  currtouched = cap.touched();

  analogWrite(led,fade);

  if(sensorValue<550 && sensorValue >480){
      fade = fade - 50;
  } 
  if (fade <=5) {
    fade=0;
    estado =0;
  }
  if(sensorValue>551){
    fade = fade +55;
  }

  if(fade >=255) {
    fade =255;
    estado =1;
  }

  
 switch (estado) {

    case 1:
    digitalWrite(led,HIGH);
    digitalWrite(led,HIGH);
    break;

    case 0:
    digitalWrite(led,LOW);
    digitalWrite(led,LOW);
    break;
 }
  for (uint8_t i=0; i<12; i++) {
    Serial.print(cap.filteredData(0));
    Serial.print(",");
    Serial.print(cap.filteredData(1));
    Serial.print(",");
    Serial.print(cap.filteredData(2));
    Serial.print(",");
    Serial.print(cap.filteredData(3));
    Serial.print(",");
    Serial.println(cap.filteredData(4));
    delay(100);
    Serial.flush();
    // it if *is* touched and *wasnt* touched before, alert!
    if ((currtouched & _BV(i)) && !(lasttouched & _BV(i)) ) {

      if(i == 0 || i == 1 || i == 2 || i ==3 || i ==4) {
              
              if(cap.filteredData(0) < cap.baselineData(0) && cap.filteredData(1) < cap.baselineData(1)) {
              Serial.print(i); 
              Serial.println("Aja Click Izquierdo" );  
              }
      }
    }
    // if it *was* touched and now *isnt*, alert!
    if (!(currtouched & _BV(i)) && (lasttouched & _BV(i)) ) {
  // Serial.print(i); Serial.println(" released");
    }
  }

  // reset our state
  lasttouched = currtouched;

  // comment out this line for detailed data from the sensor!
  return;
  
  // debugging info, what
  Serial.print("\t\t\t\t\t\t\t\t\t\t\t\t\t 0x"); Serial.println(cap.touched(), HEX);
  Serial.print("Filt: ");
  for (uint8_t i=0; i<12; i++) {
    Serial.print(cap.filteredData(i)); Serial.print("\t");
  }
  Serial.println();
  Serial.print("Base: ");
  for (uint8_t i=0; i<12; i++) {
    Serial.print(cap.baselineData(i)); Serial.print("\t");
  }
  Serial.println();
  
  // put a delay so it isn't overwhelming
  delay(100);
}
