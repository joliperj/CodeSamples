#include "dht11.h"
#include "soilmoisture.h"
#include <Wire.h>
#define SLAVE_ADDRESS 0x40   // Define the i2c address
#define DHT_PIN 4
#define SOIL_PIN A0

dht11			DHT;
soilmoisture	Moisture;
byte			ReceivedData[14];
byte			Response[14];
bool			DataReceived;

void setup()
{
	Wire.begin(SLAVE_ADDRESS);
	Wire.onReceive(receiveData);
	Wire.onRequest(sendData);
	Serial.begin(9600);

	DataReceived = false;
}

void HandleSetPinState()
{
	pinMode(ReceivedData[1], OUTPUT);
	digitalWrite(ReceivedData[1], (byte)ReceivedData[2]);
}

void loop()
{
	delay(1000);
	DHT.read(DHT_PIN);
	Moisture.read(SOIL_PIN);

	if (DataReceived)
	{
		if (ReceivedData[0] = 1)
			HandleSetPinState();
		
		memset(ReceivedData, 0, sizeof(ReceivedData));
		DataReceived = false;
	}
}

void receiveData(int numOfBytesReceived)
{
	int indexer = 0;
	while (Wire.available())
	{
		ReceivedData[indexer] = Wire.read();
		indexer++;
	}
	DataReceived = true;
}

void sendData()
{
	Response[0] = (byte)DHT.humidity;
	Response[1] = (byte)DHT.temperature;
	Response[2] = Moisture.percentage;

	Wire.write(Response, 14);
}