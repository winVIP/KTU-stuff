#include <stdio.h>
#include <sys/types.h>
#include <unistd.h>
#include <wait.h>
#include <signal.h>
#include <stdlib.h>
static int received_sig = 0;
void il_catch_USR1( int );       /* signalo apdorojimo f-ja */
int il_child( void );            /* vaiko proceso veiksmai */
int il_parent( pid_t pid );      /* tevo proceso veiksmai */
int il_child( void ){
   sleep( 1 );
   printf( "        child: my ID = %ld\n", getpid() );
   while( 1 )
      if ( received_sig == 1 ){
          printf( "        child: Received signal from parent!\n" );
          sleep( 1 );
          printf( "        child: I'm exiting\n" );
          return 0;
      }
}
int il_parent( pid_t pid ){
   printf( "parent: my ID = %ld\n", getpid() );
   printf( "parent: my child's ID = %ld\n", pid );
   sleep( 3 );
   kill( pid, SIGUSR1 );
   printf( "parent: Signal was sent\n" );
   wait( NULL );
   printf( "parent: exiting.\n" );
   return 0;
}
void il_catch_USR1( int snum ) {
   received_sig = 1;
   if(snum == SIGUSR1){
	system("who");
	printf( "SIGUSR1 caught %d\n", snum );
	kill( getpid(), SIGUSR2);

   }
}
void il_catch_USR2( int snum ) {
   received_sig = 1;
   if(snum == SIGUSR2){
	kill(snum, SIGKILL);
	sleep(5);
	printf( "SIGUSR2 caught %d\n", snum );
   }
}
int main( int argc, char **arg ){
   pid_t  pid;
   printf( "(C) 2013 Ingrida Lagzdinyte-Budnike, %s\n", __FILE__ );
   signal(SIGUSR1, il_catch_USR1);
   signal(SIGUSR2, il_catch_USR2);
   switch( pid = fork() ){
      case 0:                                         /* fork() grazina 0 vaiko procesui */
         il_child();
         break;
      default:                                        /* fork() grazina vaiko PID tevo procesui */
         il_parent(pid);
         break;
      case -1:                                        /* fork() nepavyko */
         perror("fork");
         exit(1);
   }
   exit(0);
}
