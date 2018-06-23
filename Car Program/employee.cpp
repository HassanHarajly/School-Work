///hassan harajly
//description::program stores employee information
//cis 200 10/21/17

#include <iostream>
#include <string>
using namespace std;
//description class that stores employee information
class employee {
private:int age;
		int id;
		float salary;
		string jobTitle;
		float timeOnJob;
public:
	
	employee() { age = 0; id = 0; salary = 0; }                                   // default constructor: age=0, id=0, salary=0     
	//precondition:age integer
	//post condition private variable new value assigned
	//description: enters new age for age variable

	void setAge(int ageValue) {
		if (age >= 0)//as long as age isnt 0 it will enter new values for age
		{
			age = ageValue;
		}
		else
		{
			age = 0;
		}
		employee();
	}            	//precondition:sets integer for id val
	//postcondition:new id value
	//description sets new id value

	void setId(int idValue) { id = idValue; employee(); }                 //  let id = idValue     
	//precondition: a float of a yearly salary
	//postcondition: new private variable data entry
	//enters salary of given employee 
	void setSalary(float salaryValue) {
		salary = salaryValue; employee();
	}     // salary = salaryValueint 
	int getAge();                                   // return age         
	int getId();                                      // return id     
	float getSalary();                            //  return salary};
	void setjobTitle(string title) { jobTitle.assign(title); }//sets the job title given by the user//precond:a string //post condition none
	string getJobTitle() { return jobTitle; }//returns jobtitle in a string format
	void settimeOnJob(float timer) { timeOnJob = timer; }//sets time spent on job to private variableprecondition:float postcondition:float denotiing time spent
	float gettimeOnJob() { return timeOnJob; }//returns time spent on job

};

int employee::getAge()//implementation of above functions
{
	return age;
}
int employee::getId() { return id; }//implementation of above functions

float employee::getSalary() {
	return salary;//implementation of above functions
}
const int arg2 = 2;
const int arg3 = 3;//arg2 and arg3 are size of the object array
//precondition:object array of type employee integers for size of array
//postcondition:printed information
//description:/prints all the employees information given by the user 
{
void printemployee(employee *objarr[arg2][arg3], int ar2, int ar3)
{
	static int z = 0;
	for (int j = 0; j < arg2; j++)
		for (int i = 0; i < arg3; i++)
		{
			z++;
			cout << "employee" << z << " age:" << objarr[j][i]->getAge();
			cout << " employee" << z << " id:" << objarr[j][i]->getId();
			cout << " employee" << z << " salary:" << objarr[j][i]->getSalary() << endl;

		}
	}
}
//subclass of type employee with diff data
class supervisor :public employee
{
private:
	int numberOfTeamsSupervised;
	int numberOfEmployeesSupervised;
public:
	void setteamsupervised(int supervised) { numberOfTeamsSupervised = supervised; }
	int getsupervised() { return numberOfTeamsSupervised; }
	void setnumberofemployees(int emp) { numberOfEmployeesSupervised = emp; }
	int getnumberofemployees() { return numberOfEmployeesSupervised; }

};//subclass of type employee with different data 
class staff :public employee
{
private:
	bool teamLeader;
	string applicationSupported[3];
	string jobSkill[3];
public:
	void setapplication(string app[3]) {
		for (int i = 0; i < 3; i++)
		{
			applicationSupported[i].assign(app[i]);
		}
	}
	void setjobskill(string jobskil[3]) {
		for (int i = 0; i < 3; i++)
		{
			jobSkill[i].assign(jobskil[i]);
		}
	}
};
void main()
{

	employee companyEmployees[2][3], *arg1[2][3];//initialize array
	companyEmployees[0][0].setAge(30); companyEmployees[0][0].setId(111); companyEmployees[0][0].setSalary(30000);
	companyEmployees[0][1].setAge(31); companyEmployees[0][1].setId(112); companyEmployees[0][1].setSalary(31000);
	companyEmployees[0][2].setAge(32); companyEmployees[0][2].setId(113); companyEmployees[0][2].setSalary(32000);
	companyEmployees[1][0].setAge(33); companyEmployees[1][0].setId(114); companyEmployees[1][0].setSalary(33000);
	companyEmployees[1][1].setAge(34); companyEmployees[1][1].setId(115); companyEmployees[1][1].setSalary(34000);
	companyEmployees[1][2].setAge(35); companyEmployees[1][2].setId(116); companyEmployees[1][2].setSalary(35000);

	for (int j = 0; j < arg2; j++)//sets arg1=companyemployees with pointer for easier loading
	{
		for (int i = 0; i < arg3; i++)
		{
			arg1[j][i] = &companyEmployees[j][i];
		}
	}
	printemployee(arg1, arg2, arg3);//prints data given by the array






	system("pause");

}
