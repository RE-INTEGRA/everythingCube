/*
 * IRremote: IRrecvDemo - demonstrates receiving IR codes with IRrecv
 * An IR detector/demodulator must be connected to the input RECV_PIN.
 * Version 0.1 July, 2009
 * Copyright 2009 Ken Shirriff
 * http://arcfn.com
 */

#include <IRremote.h>
char code;
int RECV_PIN = 11;
IRsend irsend;
IRrecv irrecv(RECV_PIN);
int coded,i;
 long incomingValue;
String s;
decode_results results;

void setup()
{
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
}

void loop() {

 

if(Serial.available()){


incomingValue = Serial.parseInt();


if (incomingValue > 99999999 && incomingValue < 999999999) {
 irsend.sendNEC(incomingValue, 32);
    delay(40);
irrecv.enableIRIn();
  }

if (incomingValue > 99999 && incomingValue < 999999 ) {
 irsend.sendSony(incomingValue, 12);
    delay(40);
    irrecv.enableIRIn();
}
if (incomingValue > 999999 && incomingValue < 9999999) {
 irsend.sendRC5(incomingValue, 14);
    delay(40);
  irrecv.enableIRIn();
}

if (incomingValue > 999 && incomingValue < 9999) {
 irsend.sendRC6(incomingValue, 8);
    delay(40);
    irrecv.enableIRIn();
}
}
else{

   if (irrecv.decode(&results)) {
    Serial.println(results.value);
    irrecv.resume(); // Receive the next value

}
}
} 
     

