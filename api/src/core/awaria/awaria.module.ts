import { Module } from '@nestjs/common';
import { AwariaController } from './controllers/awaria.controller';
import { AwariaService } from './services/awaria.service';

@Module({
  controllers: [AwariaController],
  providers: [AwariaService]
})
export class AwariaModule {}
