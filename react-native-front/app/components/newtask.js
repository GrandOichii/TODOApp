import { useRef, useState } from "react"
import { TextInput, TouchableOpacity, View, Text } from "react-native"
import api from "../api"

const NewTaskForm = (props) => {
    // TODO use refs to reset the text on TextInputs

    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')

    const titleRef = useRef(null)

    const handlePress = async () => {
        const resp = await api.post('/api/tasks/create', {
            title: title,
            description: description
        })

        props.onAdded(resp.data)

        setTitle('')
        setDescription ('')
    }

    return <View>
        <TextInput placeholder="Title" onChangeText={text => setTitle(text)} value={title}/>
        <TextInput multiline={true} placeholder="Description" onChangeText={text => setDescription(text)} value={description}/>    
        <TouchableOpacity onPress={handlePress}>
            <Text>Add task</Text>
        </TouchableOpacity>
    </View>
}

export default NewTaskForm