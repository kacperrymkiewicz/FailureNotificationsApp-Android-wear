import {
  Body,
  Controller,
  Get,
  HttpCode,
  Param,
  Post,
  UsePipes,
  ValidationPipe,
  Patch, 
  Headers
} from '@nestjs/common';
import { AwariaService } from '../services/awaria.service';
import { CreateAwariaDto } from '../dtos/create-awaria.dto';

@Controller('awarie')
export class AwariaController {
  constructor(private awariaService: AwariaService) {}
  @Get('/lista')
  awariaList() {
    return this.awariaService.awariaList();
  }
  @Get(':id')
  awariaById(@Param('id') id: string) {
    return this.awariaService.awariaById(id);
  }
  @Post('/dodaj')
  createAwaria(@Body() createAwariaDto: CreateAwariaDto) {
    return this.awariaService.createAwaria(createAwariaDto);
  }
  @Patch('/:id/podejmij')
  claimAwaria(@Param('id') id: string, @Headers('x-access-token') token: string) {
    return this.awariaService.claimAwaria(id);
  }
  @Patch('/:id/ukoncz')
  finishAwaria(@Param('id') id: string, @Headers('x-access-token') token: string) {
    return this.awariaService.finishAwaria(id)
  } 
}
