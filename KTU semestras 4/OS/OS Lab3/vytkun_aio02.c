#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>
#include <string.h>
#include <aio.h>

int file_open(const char *name);
int  file_close(int fd);
int  aio_read_start( const int d, struct aiocb *aiorp, void *buf, const int count, int rb );
int  aio_read_waitcomplete( struct aiocb *aiorp );

int  file_open(const char *name){
   int dskr;
   dskr = open( name, O_RDONLY );
   if( dskr == -1 ){
      perror( name );
      exit(1);
   }
   printf( "dskr = %d\n", dskr );
   return dskr;
}

int  file_close(int fd){
   int rv;
   rv = close( fd );
   if( rv != 0 ) perror ( "close() failed" );
   else puts( "closed" );
   return rv;
}

int  aio_read_start( const int d, struct aiocb *aiorp, void *buf, const int count, int rb ){
   int rv = 0;
   memset( (void *)aiorp, 0, sizeof( struct aiocb ) );
   aiorp->aio_fildes = d;
   aiorp->aio_buf = buf;
   aiorp->aio_nbytes = count;
   aiorp->aio_offset = rb;
   rv = aio_read( aiorp );
   if( rv != 0 ){
      perror( "aio_read failed" );
      abort();
   }
   return rv;
}

int  aio_read_waitcomplete( struct aiocb *aiorp ){
   const struct aiocb *aioptr[1];
   int rv;
   aioptr[0] = aiorp;
   rv = aio_suspend( aioptr, 1, NULL );
   if( rv != 0 ){
      perror( "aio_suspend failed" );
      abort();
   }
   rv = aio_return( aiorp );
   return rv;
}

int main( int argc, char *argv[] ){
   int d;
   if(argc != 2){
	printf("Netinkamas argumentu kiekis\n");
	exit( 255 );
   }
   struct aiocb aior;
   int size = atoi(argv[1]);
   char buffer[size];
   d =  file_open( "/dev/urandom" );
   int readBytes = 0;
   while(readBytes < size)
   {
	 aio_read_start( d, &aior, buffer, sizeof(buffer), readBytes );
	readBytes = readBytes +  aio_read_waitcomplete( &aior );
	printf( "AIO complete, %d bytes read.\n", readBytes );
   }
    file_close( d );
   return 0;
}

