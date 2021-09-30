/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_pipe02.c */

#include <stdio.h>
#include <unistd.h>
#include <signal.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include<sys/types.h>

int main( int argc, char * argv[] )
{
   pid_t  pid;
   int fd[2];
   char text[100];
   
   pipe(fd);
   
   printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
   switch( pid = fork() ){
      case 0:                                         /* fork() grazina 0 vaiko procesui */
		 printf ( "Child PPID: %d\n", getppid() );
		 printf ( "Child PID: %d\n", getpid() );
		 read(fd[0], text, sizeof(text));
		 printf("Argument: %s\n", text);
         break;
      default:                                        /* fork() grazina vaiko PID tevo procesui */
         write(fd[1], argv[1], sizeof(argv[1]));
		 printf("Parent PID: %d\n", getpid());
         break;
      case -1:                                        /* fork() nepavyko */
         perror("fork");
         exit(1);
   }
   return 0;
}