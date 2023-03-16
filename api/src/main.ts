import { ValidationPipe } from '@nestjs/common';
import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { dataSource } from './core/database/database.provider';
async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  await app.setGlobalPrefix('api');
  await app.useGlobalPipes(new ValidationPipe({ transform: true }));
  await app.listen(3000);
  await dataSource
    .initialize()
    .then(() => {
      console.log('Data Source has been initialized successfully.');
    })
    .catch((err) => {
      console.error('Error during Data Source initialization:', err);
    });
  await dataSource.synchronize();
}
bootstrap();
