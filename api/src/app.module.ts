import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { ConfigModule } from '@nestjs/config';
import { AwariaModule } from './core/awaria/awaria.module';
import { GatewayModule } from './gateway/gateway.module';
@Module({
  imports: [ConfigModule.forRoot({ isGlobal: true }), AwariaModule, GatewayModule],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
