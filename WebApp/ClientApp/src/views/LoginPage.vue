<template>
        <v-card class="px-100" height="100%">
            <v-layout column align-center>
                <img c src="@/assets/nfcLogo.png" alt="Vuetify.js" class="mb-5">
                <h1 class="headline">NFC lankomumo sistema</h1>
                <h1 class="headline">Prašome prisijungti</h1>
            </v-layout>
        </v-card>
</template>

<script lang='ts'>
import { Component, Vue } from 'vue-property-decorator';
import { RegisteredUsers } from '../models/RegisteredUsers';
import axios from 'axios';

@Component({})
export default class LoginPage extends Vue
{
    private created()
    {
        console.log('Login page created!');
    }
}
</script>
