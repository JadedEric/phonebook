import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PhoneBook } from '../models';

@Injectable({
    providedIn: 'root'
})
export class PhoneBookService {

    private phoneBookSubject = new BehaviorSubject<PhoneBook[]>([]);
    public phoneBook$ = this.phoneBookSubject.asObservable();

    private endPoint = `${environment.apiUrl}phonebook`;

    constructor(private httpClient: HttpClient) {                
    }

    public get(): void {
        this.httpClient.get<PhoneBook[]>(`${this.endPoint}`).subscribe((phoneBooks: PhoneBook[]) => {
            this.phoneBookSubject.next(phoneBooks);
        });
    }

    public getById(id: number): void {
        this.httpClient.get<PhoneBook>(`${this.endPoint}/${id}`).subscribe((phoneBook: PhoneBook) => {
            this.phoneBookSubject.next([phoneBook]);
        });
    }

    public post(phoneBook: PhoneBook): void {
        this.httpClient.post<PhoneBook>(`${this.endPoint}`, phoneBook)
            .pipe(mergeMap(() => this.httpClient.get<PhoneBook[]>(`${this.endPoint}`))).subscribe((phoneBooks: PhoneBook[]) => {
                this.phoneBookSubject.next(phoneBooks);
        });
    }

    public put(phoneBook: PhoneBook): void {
        this.httpClient.put<PhoneBook>(`${this.endPoint}`, phoneBook)
            .pipe(mergeMap(() => this.httpClient.get<PhoneBook[]>(`${this.endPoint}`))).subscribe((phoneBooks: PhoneBook[]) => {
                this.phoneBookSubject.next(phoneBooks);
        });
    }

    public delete(id: number): void {
        this.httpClient.delete(`${this.endPoint}/${id}`)
            .pipe(mergeMap(() => this.httpClient.get<PhoneBook[]>(`${this.endPoint}`))).subscribe((phoneBooks: PhoneBook[]) => {
                this.phoneBookSubject.next(phoneBooks);
        });        
    }
}