import { useState } from "react"
import { TextInput, TouchableOpacity, View, Text } from "react-native"
import api from "../api"

const NewTaskForm = (props) => {
    // TODO use refs to reset the text on TextInputs

    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')

    const handlePress = async () => {
        const resp = await api.post('/api/tasks/create', {
            title: title,
            description: description
        })

        props.onAdded(resp.data)

        // target.title.value = ''
        // target.description.value = ''
    }

    return <View>
        <TextInput placeholder="Title" onChangeText={text => setTitle(text)}/>
        <TextInput multiline={true} placeholder="Description" onChangeText={text => setDescription(text)}/>    
        <TouchableOpacity onPress={handlePress}>
            <Text>Add task</Text>
        </TouchableOpacity>
    </View>
}

export default NewTaskForm