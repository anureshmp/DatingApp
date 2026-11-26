import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { member } from '../../types/member';
import { AccountService } from './account-service';

@Service()
export class MemberService {
    private http = inject(HttpClient);
    private accountService = inject(AccountService);
    private baseUrl = environment.apiUrl;

    getMembers() {
        return this.http.get<member[]>(this.baseUrl + 'members');
    }

    getMember(id: string) {
        return this.http.get<member>(this.baseUrl + 'members/' + id);
    }

}
