#include <Wire.h>
#define SLAVE_ADDRESS 0x40   // Define the i2c address
#define PIN_LED 2

byte			ReceivedData[14];
byte			Response[14];
bool			DataReceived;

void setup()
{
	pinMode(PIN_LED, OUTPUT);

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

	if (DataReceived)
	{
		if (ReceivedData[0] = 1)
			HandleSetPinState();
		
		memset(ReceivedData, 0, sizeof(ReceivedData));
		DataReceived = false;
	}

	Response[0] = digitalRead(PIN_LED);
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
	Wire.write(Response, 14);
}