#include <stdio.h>
#include <sys/wait.h>
#include <unistd.h>

main(){
	int child;
	child = fork();

	if(child==-1){
		perror("can't fork"); exit(1);
	}
	else if(child == 0){
		if((child=fork())==0){
			printf("*------*------* %d \n",getpid());
		 	exit(0);
		}
		wait(5);
		printf("*------* %d \n",getpid());
	 	exit(0);

	}
	else{
		wait(5);
		printf("*%d \n", getpid());
	 	exit(0);

	}


}
