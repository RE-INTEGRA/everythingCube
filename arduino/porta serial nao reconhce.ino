#include <IRremote.h>  
  const int ledIR = 3; 
int RECV_PIN = 11;  
float armazenavalor;  
int pinoledvermelho = 5;  
int pinoledverde = 7;  
String incomingByte ;
IRrecv irrecv(11);  
decode_results results;  
  IRsend irsend;
void setup()  
{  
  pinMode(pinoledvermelho, OUTPUT);   
  pinMode(pinoledverde, OUTPUT);  
  Serial.begin(9600);  
  irrecv.enableIRIn(); // Inicializa o receptor IR  
}  
   
void loop()  
{  
if (Serial.available() > 0) {
                // read the incoming byte:
                incomingByte = Serial.readString();

                // say what you got:
                Serial.print("I received: ");
               Serial.print(incomingByte);
                
                if(incomingByte == "a"){
                  Serial.println("funfa");  
                  irsend.sendNEC(0x20DFC03F, 32);
                }
                
                if(incomingByte == "b"){
                if (irrecv.decode(&results))  {  
    Serial.print("A0:");  
    Serial.println(results.value, HEX);  
    armazenavalor = (results.value);  
    irrecv.resume(); //Le o próximo valor 
}
}
}


                
        
}


 
