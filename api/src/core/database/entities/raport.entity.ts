import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn, JoinColumn, OneToOne } from "typeorm"
import { Awaria } from "./awaria.entity";
@Entity()
export class Raport {
    @PrimaryGeneratedColumn({
        type: "int",
    })
    id: number;

    @Column({
        type: "datetime",
        nullable: false,
    })
    czas: string;

    @OneToOne(type => Awaria)
    @JoinColumn({name: "id_awarii"})
    awaria: Awaria
}