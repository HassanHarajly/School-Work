//hassan harajly
//10/21/17 cis 200
//description this program stores vehicle information inside of a series of inhereited classes



#include <iostream>

using namespace std;

class vehicle {
private:
	int age;
	float price;
public:
	//default constructor that initializes the values
	vehicle() { age = 0; price = 0; }
	//precondition:integer denoting age
	//postcondition:age variable assignment
	//description:sets private variable age
	void setage(int agehold) { age = agehold; }
	//precondition: float denoting price
	//postcondition: price variable assignment
	//description: sets private variable price
	void setprice(float pricehold) { price = pricehold; }
	int getage() { return age; }//returns age variable
	float getprice() { return price; }//returns price variable
};
//class inherits from vehicle has the same attributes
class car :public vehicle
{
private:
	bool raceCarStatus;
public:
	//constructor must call constructor in base class since it isnt inherited
	//precondition:racecar bool
	//postcondition:racecarstatus assignment
	//description: sets racecarstatus
	car() { vehicle::vehicle(); raceCarStatus = false; }
	void setRaceCarStatus(bool race) { raceCarStatus = race; }
	bool getRaceCarStatus() { return raceCarStatus; }//returns the racecar status as a 0 or 1


};
class truck :public vehicle
{
private:
	bool dieselTypeStatus;
public:
	//constructor must call constructor in base class since it isnt inherited
	truck() { vehicle::vehicle(); dieselTypeStatus = false; }//preconditon dieseltype status initialization
															 //postcondition private variable initialization
	void setDieselTypeStatus(bool status) { dieselTypeStatus = status; }//sets dieseltype status as given bool by input
	bool getDieselTypeStatus() { return dieselTypeStatus; }//returns a 0 or 1
};

int main()
{//stub main to test all functions and inheritance of various methods

	vehicle v;
	v.setage(5);
	v.setprice(95.32);

	cout << "vehicle age is " << v.getage() << endl;
	cout << "vehicle price is " << v.getprice() << endl;
	car c;
	c.setage(8);
	c.setprice(4932.02);
	c.setRaceCarStatus(false);
	cout << "age of the CAR: " << c.getage() << endl;
	cout << "price of the CAR: " << c.getprice() << endl;
	cout << "racecar status(1 for yes 0 for no): " << c.getRaceCarStatus() << endl;

	truck t;
	t.setage(53342);
	t.setprice(124932.02);
	t.setDieselTypeStatus(true);
	cout << "age of the TRUCK: " << t.getage() << endl;
	cout << "price of the TRUCK: " << t.getprice() << endl;
	cout << "diesel fuel status(1 for yes 0 for no): "
		<< t.getDieselTypeStatus() << endl;


	system("pause");
	return 0;
}
