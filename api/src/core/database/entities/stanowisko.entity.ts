import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn } from "typeorm"

@Entity()
export class Stanowisko {
    @PrimaryGeneratedColumn({
        type: "int",
    })
    id: number;

    @Column({
        type: "varchar",
        nullable: false,
    })
    kod: string;

    @Column({
        type: "varchar",
        length: 999,
        nullable: false,
    })
    opis: string;

}