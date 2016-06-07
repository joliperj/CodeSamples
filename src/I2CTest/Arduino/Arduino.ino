#include "DHT.h"
#include <Wire.h>
#define SLAVE_ADDRESS 0x40   // Define the i2c address
#define DHTPIN 4
#define DHTTYPE DHT11

DHT dht(DHTPIN, DHTTYPE);

char temp[5];
char humid[5];
void setup()
{
	Serial.begin(9600);
	Wire.begin(SLAVE_ADDRESS);
	delay(2000);
}

void loop()
{
	delay(400);
	float h = dht.readHumidity();
	delay(400);
	float t = dht.readTemperature();

	dtostrf(t, 4, 2, temp);
	dtostrf(h, 4, 2, humid);

	Serial.print("temp: ");
	Serial.println(temp);
	Serial.print("humidity: ");
	Serial.println(humid);

	Wire.onRequest(sendData);
	delay(2000);

}

void sendData()
{
	Wire.write(temp);
}