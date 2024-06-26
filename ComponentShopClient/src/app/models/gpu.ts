import { Product } from "./product";

export class Gpu extends Product {
    slot: string;
    memory: number;
    ports: Array<string> = [];
};