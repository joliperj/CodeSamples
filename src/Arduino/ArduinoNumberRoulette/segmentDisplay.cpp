// 
// 
// 

#include "segmentDisplay.h"

void segmentDisplay::displayNumber(int number)
{
#define DISPLAY_BRIGHTNESS  5000

#define DIGIT_ON  HIGH
#define DIGIT_OFF  LOW

	pinMode(segA, OUTPUT);
	pinMode(segB, OUTPUT);
	pinMode(segC, OUTPUT);
	pinMode(segD, OUTPUT);
	pinMode(segE, OUTPUT);
	pinMode(segF, OUTPUT);
	pinMode(segG, OUTPUT);

	pinMode(digit1, OUTPUT);
	pinMode(digit2, OUTPUT);
	pinMode(digit3, OUTPUT);
	pinMode(digit4, OUTPUT);

	for (int digit = 4; digit > 0; digit--) {

		//Turn on a digit for a short amount of time
		switch (digit) {
		case 1:
			digitalWrite(digit1, DIGIT_ON);
			break;
		case 2:
			digitalWrite(digit2, DIGIT_ON);
			break;
		case 3:
			digitalWrite(digit3, DIGIT_ON);
			break;
		case 4:
			digitalWrite(digit4, DIGIT_ON);
			break;
		}

		//Turn on the right segments for this digit
		lightNumber(number%10);
		number /= 10;

		delayMicroseconds(DISPLAY_BRIGHTNESS);
		//Display digit for fraction of a second (1us to 5000us, 500 is pretty good)

		//Turn off all segments
		lightNumber(10);

		//Turn off all digits
		digitalWrite(digit1, DIGIT_OFF);
		digitalWrite(digit2, DIGIT_OFF);
		digitalWrite(digit3, DIGIT_OFF);
		digitalWrite(digit4, DIGIT_OFF);
	}
}

//Given a number, turns on those segments
//If number == 10, then turn off number
void segmentDisplay::lightNumber(int numberToDisplay) {

#define SEGMENT_ON  LOW
#define SEGMENT_OFF HIGH

	switch (numberToDisplay) {

	case 0:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_ON);
		digitalWrite(segF, SEGMENT_ON);
		digitalWrite(segG, SEGMENT_OFF);
		break;

	case 1:
		digitalWrite(segA, SEGMENT_OFF);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_OFF);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_OFF);
		digitalWrite(segG, SEGMENT_OFF);
		break;

	case 2:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_OFF);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_ON);
		digitalWrite(segF, SEGMENT_OFF);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 3:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_OFF);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 4:
		digitalWrite(segA, SEGMENT_OFF);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_OFF);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_ON);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 5:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_OFF);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_ON);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 6:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_OFF);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_ON);
		digitalWrite(segF, SEGMENT_ON);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 7:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_OFF);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_OFF);
		digitalWrite(segG, SEGMENT_OFF);
		break;

	case 8:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_ON);
		digitalWrite(segF, SEGMENT_ON);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 9:
		digitalWrite(segA, SEGMENT_ON);
		digitalWrite(segB, SEGMENT_ON);
		digitalWrite(segC, SEGMENT_ON);
		digitalWrite(segD, SEGMENT_ON);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_ON);
		digitalWrite(segG, SEGMENT_ON);
		break;

	case 10:
		digitalWrite(segA, SEGMENT_OFF);
		digitalWrite(segB, SEGMENT_OFF);
		digitalWrite(segC, SEGMENT_OFF);
		digitalWrite(segD, SEGMENT_OFF);
		digitalWrite(segE, SEGMENT_OFF);
		digitalWrite(segF, SEGMENT_OFF);
		digitalWrite(segG, SEGMENT_OFF);
		break;
	}
}