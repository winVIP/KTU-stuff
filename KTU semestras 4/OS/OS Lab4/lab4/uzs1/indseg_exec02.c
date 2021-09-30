#include <stdio.h>
#include <unistd.h>
#include <string.h>
#include <errno.h>
#include <stdlib.h>

int main(){

	const char *command[] ={ "touch hello.sh", "echo \"#/bin/sh\" > hello.sh",  "echo \"echo Hello, World!\" >> hello.sh", "chmod u+x hello.sh" };
	system(command[0]);
	perror("");
	system(command[1]);
	perror("");
	system(command[2]);
	perror("");
	system(command[3]);
	perror("");

	system("./hello.sh");
	perror("");
	return 0;
}
