#include <IRremote.h>  
  
int RECV_PIN = 11;  
float armazenavalor;  
 const int ledIR = 3; 
IRsend irsend;
String incomingByte;
IRrecv irrecv(RECV_PIN);  
decode_results results;  
long nada;
  
void setup()  
{  


  Serial.begin(9600);  
  irrecv.enableIRIn(); // Inicializa o receptor IR  
   pinMode(ledIR ,OUTPUT); // saída do infravermelho  
}    
   
void loop()  
{  
 if(Serial.available()>0){  
    
   
                // read the incoming byte:
                incomingByte = Serial.readString();

             
                Serial.print(incomingByte);
                
                irsend.sendNEC(0x20DFC03F, 32);
                 
 }

if (irrecv.decode(&results))  {  
    Serial.print("A0:");  
    Serial.println(results.value, HEX);  
    armazenavalor = (results.value);  
    irrecv.resume(); //Le o próximo valor 
}
    
    
  
 

  
  
}  
