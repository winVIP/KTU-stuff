#include <stdio.h>
#include <sys/wait.h>
#include <unistd.h>

main(){
	int child;
	child = fork();

	if(child==-1){
		perror("can't fork"); exit(1);
	}
	else if(child != 0){
		wait(5);
		printf("*%d \n", getpid());
	 	exit(0);

	}
	else{
		for (int i=0; i<2; i++)
		  {
			if((child=fork())==0){
				printf("*-----* %d \n",getpid());
		 		exit(0);
			}
		}
	}


}
