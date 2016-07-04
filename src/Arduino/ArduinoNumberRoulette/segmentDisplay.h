// segmentDisplay.h

#ifndef _SEGMENTDISPLAY_h
#define _SEGMENTDISPLAY_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class segmentDisplay
{
public:
	void displayNumber(int number);
	int digit1, digit2, digit3, digit4;
	int segA, segB, segC, segD, segE, segF, segG;
private:
	void lightNumber(int number);
};

#endif

