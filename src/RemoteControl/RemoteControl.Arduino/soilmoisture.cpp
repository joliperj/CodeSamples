// FILE: soilmoisture.cpp
// VERSION: 0.1
// PURPOSE: Soilmoisture Sensor library for Arduino
// LICENSE: GPL v3 (http://www.gnu.org/licenses/gpl.html)

#include "soilmoisture.h"

void soilmoisture::read(int pin)
{
	float val = 1023 - (int)analogRead(pin);
	percentage = val / 1023 * 100;
}