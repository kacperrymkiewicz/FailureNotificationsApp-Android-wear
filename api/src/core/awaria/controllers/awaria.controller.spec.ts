import { Test, TestingModule } from '@nestjs/testing';
import { AwariaController } from './awaria.controller';

describe('AwariaController', () => {
  let controller: AwariaController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [AwariaController],
    }).compile();

    controller = module.get<AwariaController>(AwariaController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
