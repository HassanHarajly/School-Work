#include <iostream>
#include <string>

using namespace std;


class account {

private:
	long accNum = 0;
	double balance = 0;
public:
	account(){}
	long getAcct()
	{
		return accNum;
	}
	double getBalance() {
		return balance;
	
	cout << "inside balance"<<balance<<" yo ";
	}
	int setAcct(long acct)
	{
		try {
			if (acct >= 0)
			{
				accNum =+ acct;
			}
			else
			{
				throw "account number cant be negative!";
			}
		}
		catch (string x)
		{
			cout << x << endl;
			return -1;
		}

	}
	int setBalance(double balanc)
	{
		try 
		{
			if (balanc >= 0)
			{
				balance =+ balanc;
			}
			else
			{
				throw "balance Cannot be negative!";
			}
		}
		catch (string g)
		{
			cout << g << endl;
			return -1;
		}

		return 0;
	}
account(long acct, double bal)
{
	

	try
	{
		if (bal >= 0)
		{
			balance = bal;
			
		}
		else
		{
			throw "balance Cannot be negative!";
		}
	}
	catch (string g)
	{
		cout << g << endl;
	
	}
	
		try {
			if (acct >= 0)
			{
				accNum = acct;
			}
			else
			{
				throw "account number cant be negative!";
			}
		}
		catch (string x)
		{
			cout << x << endl;
			
		}
	
}

string toString()
{

	string Balance = to_string(accNum);
	return Balance;
}

virtual double computeintr(int years)
{
	return 0;
}
};

class checking:public account
{
private:
	int minIntrBalance = 0;
	int intRate = 0;
public:
	int	getMinIntrBalance() { return minIntrBalance; }
	int setMinIntrBalance(int lowestBalance)
	{
		try {
			if (lowestBalance >= 0)
			{
				minIntrBalance = lowestBalance;
			}

			else
			{
				throw "the minimum balance cant be less than 0!";
				return -1;
			}
		}
		catch (string k)
		{
			cout << k << endl;
		}
	}
	int getRate() { return intRate; }
	int setRate(int rate)
	{
		try {
			if (rate >= 0)
			{
				intRate = rate;
			}
			else { throw "rate cannot be negative!"; }
		}
		catch (string k)
		{
			cout << k << endl;
			return -1;
		}
	}
	double computeIntr(int years)
	{
		
		float temp= (float)intRate*.01;
		float tempb=getBalance();
		minIntrBalance =getBalance()+ tempb*temp*years;
		return minIntrBalance;
	}
	checking(long acc, double bal)
	{
		account a(acc, bal);
		setBalance(bal);
		setAcct(acc);

	}
	string toString()
	{
		
		return to_string(getAcct());
	}
};

class saving:public account
{
     private:
	double intRate = 0;
   public:
	   double getRate() { return intRate; }
	   void setRate(double rate) {
		   try {
			   if (rate >= 0)
			   {
				   intRate = rate;
			   }
			   else
				   throw "interest rate cannot be negative!";
			  
		   }
		   catch (string x)
		   {
			   cout << x << endl;
		   }
	   }
	   double computeIntr(int years)
	   {
		   float minIntrBalance = 0;
		   float temp = (float)intRate*.01;
		   float tempb = getBalance();
		   minIntrBalance = getBalance() + tempb*temp*years;
		   return minIntrBalance;
	   }
	   saving(long acc, double bal)
	   {
		   account a(acc, bal);
		   setBalance(bal);
		   setAcct(acc);

	   }
	   string toString()
	   {
		   return to_string(getAcct());
	   }

};



int main()
{
	account * p[100];
	for (int i = 0; i < 100; i++)
	{
		p[i] = &accounts;
	}
	for (int i = 0; i < 5; i++)
	{
		p[i] = &chek;
		p[i]->setAcct(100 + i);
	}

	for (int i = 0; i < 5; i++)
	{

		p[i + 4] = &sav;
		p[i + 4]->setAcct(200 + i);
		p[20] = &sav;
		
		p[20]->setRate(4);
	}



	system("pause");
	return 0;
}