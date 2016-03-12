int ledPin = 13;
String lampada ;

void setup(){
  pinMode(ledPin,OUTPUT);
  Serial.begin(9600);
}
void loop(){
  if(Serial.available() > 0){
    lampada = Serial.readString();

Serial.println(lampada);
    
    if(lampada == "a"){

        Serial.println("funfando"); 
         digitalWrite(ledPin,HIGH);
         
         
    }

    if(lampada == "b"){

        Serial.println("funfando"); 
         digitalWrite(ledPin,LOW);
         
         
    }
  }
}

