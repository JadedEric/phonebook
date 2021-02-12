import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { PhoneBook } from './models';
import { PhoneBookService, PhoneBookEntryService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'phonebook';

  public phoneBook$ = this.phoneBookService.phoneBook$;
  public phoneBookEntrie$ = this.phoneBookEntryService.phoneBookEntrie$;

  public addBook = false;
  public allowEntryAdd = false;

  public newEntryForm = new FormGroup({});

  private selectedPhoneBookId = -1;

  public alphabet = [
    'All', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
  ];

  public showEntryForm = false;

  constructor(
    private fb: FormBuilder,
    private phoneBookEntryService: PhoneBookEntryService,
    private phoneBookService: PhoneBookService) {
      this.phoneBookService.get();      
    }

  ngOnInit(): void {
    
    this.newEntryForm = this.fb.group({
      name: '',
      phoneNumber: '',
      phoneBookId: -1
    });
  }

  public newBook(): void {
    this.addBook = true;
  }

  public saveBook(name: string): void {
    const phoneBook: PhoneBook = {
      name
    };

    this.phoneBookService.post(phoneBook);

    this.addBook = false;
  }

  public changePhoneBook(change: MatSelectChange): void {
    this.phoneBookEntryService.getByPhoneBookId(change.value);
    this.selectedPhoneBookId = change.value;
    this.allowEntryAdd = true;

    this.newEntryForm.get('phoneBookId')?.patchValue(change.value);
  }

  public addEntry(): void {
    this.showEntryForm = true;
  }

  public saveEntry(): void {
    this.phoneBookEntryService.post(this.newEntryForm.value);
    this.newEntryForm.reset();
    this.showEntryForm = false;
  }

  public filter(term: string): void {
    this.phoneBookEntryService.filter(this.selectedPhoneBookId, term);
  }
}
