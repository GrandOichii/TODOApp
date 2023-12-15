import { useRef, useState } from "react";
import { View, Text, TextInput, TouchableOpacity } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import api from '../api'

const TextInputStyle = { 
    borderColor: 'black', 
    borderWidth: 1 
}

const Login = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [failed, setFailed] = useState(false)

    const handlePress = async () => {
        try {
            const resp = await api.post('/api/users/login', {
                username: username, 
                password: password
            })
            setFailed(false)
            console.log(resp.data);
        } catch (e) {
            setFailed(true)
        }
    }

    return <SafeAreaView>
        <TextInput onChangeText={value => setUsername(value)} placeholder="Username" style={TextInputStyle} />
        <TextInput onChangeText={value => setPassword(value)} placeholder="Password" style={TextInputStyle} />
        <TouchableOpacity onPress={handlePress}>
            <Text>Login</Text>
        </TouchableOpacity>
        { failed && <Text style={{color: 'red'}}>Failed to log in</Text>}
    </SafeAreaView>
}

export default Login