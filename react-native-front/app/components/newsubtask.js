import { useState } from "react"
import { TextInput, TouchableOpacity, View, Text } from "react-native"
import api from "../api"

const NewSubtask = (props) => {
    
    const [title, setTitle] = useState('')

    const handlePress = async () => {
        const resp = await api.post('/api/tasks/subtask/add', {
            title: title,
            taskID: props.task.id
        })
        props.onUpdated(resp.data)
    }

    return <View style={{flexDirection: "row"}}>
        <TextInput placeholder="Subtask" onChangeText={text => setTitle(text)}/>
        <TouchableOpacity onPress={handlePress}>
            <Text>Add</Text>
        </TouchableOpacity>
    </View>
}

export default NewSubtask