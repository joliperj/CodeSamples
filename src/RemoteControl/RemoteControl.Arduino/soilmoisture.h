// FILE: soilmoisture.h
// VERSION: 0.1
// PURPOSE: Soil moisture sensor library for Arduino
// LICENSE: GPL v3 (http://www.gnu.org/licenses/gpl.html)

#ifndef soilmoisture_h
#define soilmoisture_h

#if defined(ARDUINO) && (ARDUINO >= 100)
#include <Arduino.h>
#else
#include <WProgram.h>
#endif

class soilmoisture
{
public:
	void read(int pin);
	int percentage;
};
#endif
//
// END OF FILE
//