int ledPin = 13;
String lampada ;
int v =0;
int i =0;
void setup(){
  pinMode(ledPin,OUTPUT);
  pinMode(A5,INPUT);
  Serial.begin(9600);
}
void loop(){

   digitalWrite(ledPin,HIGH);
Serial.println(analogRead(A5)); 

  
  if (Serial.available() > 0){
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

