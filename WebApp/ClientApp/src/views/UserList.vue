<template>
  <v-container fluid>
    <Registration/>
    <v-slide-y-transition mode="out-in">
      <v-layout column>
        <h1>Užregistruotų vartotjų sąrašas</h1>
      <v-layout row>
        <v-flex xs6>
        <p >Čia galima peržiūrėti sistemos vartotojus, keisti jų duomenis</p>
        </v-flex>
        <v-flex offset-xs6 order-lg2>
        </v-flex>
      </v-layout>
        <v-data-table
            :headers="headers"
            :items="users"
            hide-actions
            :loading="loading"
            class="elevation-3"
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
              <td><EditUser :userObj = "props.item"/></td>
              <!-- <td><DeleteUser :userObj = "props.item"/></td>    -->
            </template>
          </v-data-table>

      </v-layout>
    </v-slide-y-transition>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
//import { Device } from '../models/Device';
//import { Classroom } from '../models/Classroom';
//import HelloWorld from '../components/HelloWorld.vue'
import axios from 'axios';
import '../components/login/auth-header';
import { authHeader } from '../components/login/auth-header';
import EditUser from './PopupDialogs/EditUser.vue';
import Registration from './Registration.vue';
import { RegisteredUsers } from '../models/RegisteredUsers';

@Component({
  components: {
    EditUser,
    Registration,
  },
})
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
    { text: 'UID', value: 'uid'},
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
