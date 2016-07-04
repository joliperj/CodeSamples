/*
 Name:		ArduinoNumberRoulette.ino
 Created:	6/30/2016
 Author:	TheProgrammator
*/

#include "segmentDisplay.h"

segmentDisplay	segDisplay;

int		nextUpdate;
int		timeToRun;
int		rouletteNumber;

void setup() {
	segDisplay.digit1 = 12;	//PWM Display pin 1
	segDisplay.digit2 = 11;	//PWM Display pin 2
	segDisplay.digit3 = 10;	//PWM Display pin 6
	segDisplay.digit4 = 9;	//PWM Display pin 8

	segDisplay.segA = 2;
	segDisplay.segB = 3;
	segDisplay.segC = 4;
	segDisplay.segD = 5;
	segDisplay.segE = 6;
	segDisplay.segF = 7;
	segDisplay.segG = 8;

	nextUpdate = 0;
	timeToRun = 40000; // milliseconds
	rouletteNumber = 0;
	
	randomSeed(analogRead(A0));

}

void loop() 
{
	if (millis() > nextUpdate && millis() < timeToRun)
	{
		nextUpdate = millis() + (millis() * 0.1) / 10;
		rouletteNumber = random(1000, 9999);
	}
	segDisplay.displayNumber(rouletteNumber);
}