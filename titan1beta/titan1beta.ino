/*
  AnalogReadSerial
  Reads an analog input on pin 0, prints the result to the serial monitor.
  Graphical representation is available using serial plotter (Tools > Serial Plotter menu)
  Attach the center pin of a potentiometer to pin A0, and the outside pins to +5V and ground.

  This example code is in the public domain.
*/const int sensor = 2;
int ledIzq = 12;
int ledDer = 13;
int cantidadBrillo;
int fade = 5;
int estado;
int litro_Hora;
volatile int pulsos = 0;
unsigned long tiempoAnterior = 0;
unsigned long pulsos_Acumulados = 0;
int litro;
int vib = 7; //Vibrador
int hackSensor = 1;
int pinR1 = 3;
int pinG1 = 5;
int pinB1 = 6;
int pinR2 = 9;
int pinG2 = 10;
int pinB2 =11;

float brilloRGBR1;
float brilloRGBG1;
float brilloRGBB1;
float brilloRGBR2;
float brilloRGBG2;
float brilloRGBB2;
float brilloRGBR3;
float brilloRGBG3;
float brilloRGBB3;

//variable de sensor de temperatura
int tempC;
int tempPin = A1;

void flujo()
{
  pulsos++;
}

// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(115200);
  pinMode(ledIzq,OUTPUT);
  pinMode(ledDer,OUTPUT);
  pinMode(sensor, INPUT_PULLUP); //Caudalimetro conectado en el pin declarado anteriormente
pinMode(vib, OUTPUT);
pinMode(pinR1,OUTPUT);
pinMode(pinG1,OUTPUT);
pinMode(pinB1,OUTPUT);
pinMode(pinR2,OUTPUT);
pinMode(pinG2,OUTPUT);
pinMode(pinB2,OUTPUT);
pinMode(pinR3,OUTPUT);//Vibrador declarao anteriormente
pinMode(pinG3,OUTPUT);
pinMode(pinB3,OUTPUT);
interrupts();
attachInterrupt(digitalPinToInterrupt(sensor),flujo,RISING);
tiempoAnterior = millis();  
brilloRGBR1 = 255;
brilloRGBR2 = 255;
brilloRGBR3 = 255;
brilloRGBG1 = 255;
brilloRGBG2 = 255;
brilloRGBG3 = 255;
brilloRGBB1 = 0;
brilloRGBB2 = 0;
brilloRGBB3 = 0;

analogWrite( pinR1, brilloRGBR1);
analogWrite( pinG1, brilloRGBG1);
analogWrite( pinB1, brilloRGBB1);
analogWrite( pinR2, brilloRGBR2);
analogWrite( pinG2, brilloRGBG2);
analogWrite( pinB2, brilloRGBB2);
analogWrite( pinR3, brilloRGBR3);
analogWrite( pinG3, brilloRGBG3);
analogWrite( pinB3, brilloRGBB3);

}

// the loop routine runs over and over again forever:
void loop() {
  // read the input on analog pin 0:
  tempC = analogRead(tempPin);

  float voltage = 5.0 * tempC;
  voltage /= 1024.0;

  int temperatureC = (int)((voltage - 0.5)*100);
  
  int sensorValue = analogRead(A0);
 
  analogWrite(ledIzq,fade);
  analogWrite(ledDer,fade);
  analogWrite( pinR1, brilloRGBR1);
  analogWrite( pinG1, brilloRGBG1);
  analogWrite( pinB1, brilloRGBB1);
  analogWrite( pinR2, brilloRGBR2);
  analogWrite( pinG2, brilloRGBG2);
  analogWrite( pinB2, brilloRGBB2);
  analogWrite( pinR3, brilloRGBR3);
  analogWrite( pinG3, brilloRGBG3);
  analogWrite( pinB3, brilloRGBB3);
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
  // print out the value you read:
  //Serial.println(fade);

 switch (estado) {

    case 1:
    digitalWrite(ledIzq,HIGH);
    digitalWrite(ledDer,HIGH);
    break;

    case 0:
    digitalWrite(ledIzq,LOW);
    digitalWrite(ledDer,LOW);
    break;
 }
         // delay in between reads for stability
  if(millis() - tiempoAnterior > 1000){  
  tiempoAnterior = millis();
  pulsos_Acumulados += pulsos;
  litro_Hora = (pulsos * 60 / 7.5);
  litro = (int)(pulsos_Acumulados * 1.0/450);
  pulsos = 0;  

//Serial.println(">CAUDALIMETREO YF-S201");
//Serial.println("***********************");
//Serial.print("->");
//Serial.print(litro_Hora,DEC);
//Serial.println(" L/Hora");
//Serial.print("->");
//Serial.print(litro);
Serial.print(temperatureC);
Serial.print(",");
Serial.println(litro);
Serial.flush();
delay(10);

/**
 * Condicional para alertar al 50%
 */
if(litro>=1 && litro <2){
  if (hackSensor >= 1 && hackSensor<2){
      digitalWrite(vib,HIGH);
      delay(1000);
      digitalWrite(vib,LOW);
      hackSensor++;
  }
}
/**
 * Condicional para alertar al 75%
 */
if(litro>=2){
  
  if (hackSensor == 2){
    brilloRGBR1 = 125;
    brilloRGBR2 =125;
    brilloRGBG1 =125;
    brilloRGBG2 =125;
    brilloRGBB1 =255;
    brilloRGBB2 =255;

  
    while(hackSensor <=3){
      digitalWrite(vib,HIGH);
      delay(1000);
      digitalWrite(vib,LOW);
    
      hackSensor++;
    }
  } 
  
 /**
 * Condicional para alertar al 90%
 */
    if(litro>=3){
        //Serial.println(hackSensor);
        if (hackSensor == 4){
          brilloRGBR1 = 0;
          brilloRGBR2 =0;
          brilloRGBG1 =255;
          brilloRGBG2 =255;
          brilloRGBB1 =255;
          brilloRGBB2 =255;
            while (hackSensor <= 6){ 
                digitalWrite(vib,HIGH);
               // delay(1000);
                digitalWrite(vib,LOW);
                hackSensor++;
            }
        }
    
    }
   }
  }
}
