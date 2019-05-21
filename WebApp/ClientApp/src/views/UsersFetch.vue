<template>
  <div>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-layout column>
        <h1>Užregistruotų studentų sąrašas</h1>
        <p>Čia galite rasti informaciją apie studentus</p>
        <v-data-table
            :headers="headers"
            :items="users"
            hide-actions
            :loading="loading"
            class="elevation-1"
          >
            <v-progress-linear v-slot:progress color="blue" indeterminate></v-progress-linear>
            <template v-slot:items="props">
              <td>{{ props.item.userName }}</td>
              <td>{{ props.item.name }}</td>
              <td>{{ props.item.surname }}</td>
              <td>{{ props.item.group }}</td>
              <td>{{ props.item.studentCode }}</td>
              <td>{{ props.item.email }}</td>
              <td>{{ props.item.uid }}</td>
              <td class="justify-center layout px-0">
            </td>
            </template>
          </v-data-table>

      </v-layout>
    </v-slide-y-transition>
  </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { RegisteredUsers } from '../models/RegisteredUsers';
import axios from 'axios';
import { authHeader } from '../components/login/auth-header';

@Component({})
export default class FetchDataView extends Vue {
  private loading: boolean = true;
  private users: RegisteredUsers[] = [];
  private headers = [
    { text: 'Prisijungimo vardas', value: 'userName' },
    { text: 'Vardas', value: 'name' },
    { text: 'Pavardė', value: 'surname' },
    { text: 'Grupė', value: 'group' },
    { text: 'Vidko', value: 'studentCode' },
    { text: 'El. Paštas', value: 'email' },
    { text: 'Pažymėjimo ID', value: 'uid'},
    { text: '', value: ''},
  ];

  private created() {
    this.fetchUsers();
  }

  private fetchUsers() {
    const headers = {...authHeader()};
    axios.get<RegisteredUsers[]>('api/userlist/getusers', {headers:headers})
      .then((response) => {
        this.users = response.data;
        this.loading = false;
      });
  }
}
</script>
