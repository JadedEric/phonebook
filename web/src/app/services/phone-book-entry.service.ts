import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PhoneBook, PhoneBookEntry } from '../models';

@Injectable({
    providedIn: 'root'
})
export class PhoneBookEntryService {
    private phoneBookEntrySubject = new BehaviorSubject<PhoneBookEntry[]>([]);
    public phoneBookEntrie$ = this.phoneBookEntrySubject.asObservable();

    private endPoint = `${environment.apiUrl}phonebookentry`;

    constructor(private httpClient: HttpClient) {                
    }

    public get(): void {
        this.httpClient.get<PhoneBookEntry[]>(`${this.endPoint}`).subscribe((phoneBookEntries: PhoneBookEntry[]) => {
            this.phoneBookEntrySubject.next(phoneBookEntries);
        });
    }

    public getById(id: number): void {
        this.httpClient.get<PhoneBookEntry>(`${this.endPoint}/${id}`).subscribe((phoneBookEntry: PhoneBookEntry) => {
            this.phoneBookEntrySubject.next([phoneBookEntry]);
        });
    }

    public getByPhoneBookId(id: number): void {
        this.httpClient.get<PhoneBookEntry[]>(`${this.endPoint}/phonebook/${id}`)
            .subscribe((phoneBookEntries: PhoneBookEntry[]) => {
                this.phoneBookEntrySubject.next(phoneBookEntries);
        });        
    }

    public post(phoneBookEntry: PhoneBookEntry): void {
        this.httpClient.post<PhoneBookEntry>(`${this.endPoint}`, phoneBookEntry)
            .pipe(mergeMap(() => this.httpClient.get<PhoneBookEntry[]>(`${this.endPoint}/phonebook/${phoneBookEntry.phoneBookId}`)))
            .subscribe((phoneBookEntries: PhoneBookEntry[]) => {
                this.phoneBookEntrySubject.next(phoneBookEntries);
        });
    }

    public put(phoneBookEntry: PhoneBookEntry): void {
        this.httpClient.put<PhoneBookEntry>(`${this.endPoint}`, phoneBookEntry)
            .pipe(mergeMap(() => this.httpClient.get<PhoneBookEntry[]>(`${this.endPoint}/phonebook/${phoneBookEntry.phoneBookId}`)))
            .subscribe((phoneBookEntries: PhoneBookEntry[]) => {
                this.phoneBookEntrySubject.next(phoneBookEntries);
        });
    }

    public delete(id: number): void {
        this.httpClient.delete(`${this.endPoint}/${id}`)
            .pipe(mergeMap(() => this.httpClient.get<PhoneBookEntry[]>(`${this.endPoint}`)))
            .subscribe((phoneBookEntries: PhoneBookEntry[]) => {
                this.phoneBookEntrySubject.next(phoneBookEntries);
        });        
    }

    public filter(phoneBookId: number, term: string): void {
        this.httpClient.get<PhoneBookEntry[]>(`${this.endPoint}/phonebook/${phoneBookId}`).subscribe((phoneBookEntries: PhoneBookEntry[]) => {            
            const filtered: PhoneBookEntry[] = [];
            
            if (term === 'All') {
                filtered.push(...phoneBookEntries);
            }
            else {
                filtered.push(...phoneBookEntries.filter((entry: PhoneBookEntry) => entry.name.toLowerCase().startsWith(term.toLowerCase())));
            }
            
            this.phoneBookEntrySubject.next(filtered);
        });
    }
}