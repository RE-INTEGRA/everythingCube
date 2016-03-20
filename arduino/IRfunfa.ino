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
 int incomingValue;

decode_results results;

void setup()
{
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
}

void loop() {

 if (Serial.available()) {


char buffer[] = {' ',' ',' ',' ',' ',' ',' '}; // Receive up to 7 bytes
 while (!Serial.available()); // Wait for characters
 Serial.readBytesUntil('n', buffer, 7);
incomingValue = atoi(buffer);



 irsend.sendNEC(incomingValue, 32);
    delay(40);
  }
  else{

 if (irrecv.decode(&results)) {
    Serial.println(results.value, HEX);
    irrecv.resume(); // Receive the next value
    
  } 
     
}
}

